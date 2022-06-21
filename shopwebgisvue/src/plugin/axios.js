'use strict'

import Vue from 'vue'
import axios from 'axios'
import store from '@/store'
import router from '@/router'

// Full config:  https://github.com/axios/axios#request-config
// axios.defaults.baseURL = process.env.baseURL || process.env.apiUrl || '';
// axios.defaults.headers.common['Authorization'] = AUTH_TOKEN;
//axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

let config = {
  //baseURL: process.env.baseURL || process.env.apiUrl || "/api",
  // timeout: 60 * 1000, // Timeout
  //withCredentials: true, // Check cross-site Access-Control
}

let LOGIN_URL = '/token'

let REFRESH_TOKEN_URL = '/token'

let ERROR_CONFLICT_MESSAGE =
  '您的账号在其他地方登录，如果不是你本人操作，请尽快修改登录密码'

const _axios = axios.create(config)

axios.defaults.baseURL = process.env.baseURL || process.env.apiUrl || '/api'

_axios.interceptors.request.use(
  function (config) {
    // Do something before request is sent
    const accesstoken = store.state.auth.accessToken || ''
    config.headers.Authorization = 'Bearer ' + accesstoken
    return config
  },
  function (error) {
    // Do something with request error
    return Promise.reject(error.response)
  }
)

// Add a response interceptor
_axios.interceptors.response.use(
  function (response) {
    if (response.data != null && response.data.ErrorCode != null) {
      if (response.data.ErrorCode !== 0) {
        return Promise.reject(response.data.Message)
      } else {
        return response.data.Data
      }
    }
    return response.data
    // Do something with response data//
  },
  function (error) {
    // Do something with response error
    if (_isInvalidToken(error.response)) {
      return _refreshToken(error.config)
    }
    return Promise.reject(error.response)
  }
)

function login(creds, redirect) {
  const data =
    'grant_type=password&username=' +
    creds.username +
    '&password=' +
    creds.password +
    '&captcha1=' +
    creds.captcha1 +
    '&captcha2=' +
    creds.captcha2
  return _axios.post(LOGIN_URL, data).then((response) => {
    // 保存登陆信息
    _storeToken(response)

    if (redirect) {
      router.push({
        name: redirect,
      })
    }
    return response.data
  })
}

function _storeToken(response) {
  const auth = store.state.auth

  auth.isLoggedIn = true
  auth.accessToken = response.access_token
  auth.refreshToken = response.refresh_token

  store.commit('UPDATE_AUTH', auth)
}

function _isInvalidToken(response) {
  const status = response.status
  const error = response.statusText
  return status == 401 && (error == 'invalid_token' || error == 'expired_token')
}

function _refreshToken(config) {
  const params =
    'grant_type=refresh_token&refresh_token=' + store.state.auth.refreshToken

  return _axios
    .post(REFRESH_TOKEN_URL, params)
    .then((resp) => {
      _storeToken(resp)
      return _retry(config)
    })
    .catch((err) => {
      return Promise.reject(err)
    })
}

function _retry(config) {
  return _axios.request(config)
}

function logout() {
  store.commit('CLEAR_ALL_DATA')

  router.push({
    name: 'login',
  })
}

function GetData(apiUrl, params) {
  if (!isNull(params)) {
    var queryString = null
    for (var key in params) {
      queryString += '&' + key + '=' + encodeURIComponent(params[key])
    }
    apiUrl += '?' + queryString.substr(1)
  }
  return _axios.get(apiUrl).then(
    function (response) {
      return response
    },
    function (errorResponse) {
      if (
        errorResponse.status == 409 &&
        errorResponse.statusText == 'unauthorized'
      ) {
        logout()
        return Promise.reject(ERROR_CONFLICT_MESSAGE)
      }

      if (errorResponse.status === 401) {
        alert('您没有权限访问该资源.')
        return
      }
      return Promise.reject(errorResponse.statusText)
    }
  )
}

/**
 * 判断一个变量是否是undefined或者null
 * @param o 需要进行判断的变量
 * @returns {boolean} 如果是undified或者null，则返回true，否则返回 false
 */
function isNull(o) {
  return o === undefined || o === null
}

Plugin.install = function (Vue) {
  Vue.prototype.axios = _axios
  Vue.prototype.$authlogin = login
  Vue.prototype.$authlogout = logout
  window.axios = _axios
  Vue.prototype.$GetData = GetData
  Object.defineProperties(Vue.prototype, {
    $axios: {
      get() {
        return _axios
      },
      // post() {
      //   return _axios;
      // }
    },
  })
}
Vue.use(Plugin)

export default Plugin
