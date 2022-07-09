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
  baseURL: process.env.baseURL || process.env.apiUrl || '/api',
  timeout: 60 * 1000, // Timeout
  withCredentials: true, // Check cross-site Access-Control
}

let LOGIN_URL = '/User/Login'

let REFRESH_TOKEN_URL = '/User/RefreshToken'

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
    if (
      response.data !== null &&
      response.data.resultCode !== null &&
      response.data.success !== null
    )
      if (response.data.ResultCode !== 200 && response.data.success !== true) {
        return Promise.reject(response.data.errorMessage)
      } else {
        return response.data.resultData
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
  const data = 'userName=' + creds.username + '&userPassWord=' + creds.password
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
  auth.accessToken = response.accessToken
  auth.refreshToken = response.refreshToken

  store.commit('UPDATE_AUTH', auth)
}

function _isInvalidToken(response) {
  const status = response.status
  return status == 401
}

function _refreshToken(config) {
  debugger
  const params = '?refreshToken=' + store.state.auth.refreshToken

  return _axios
    .get(REFRESH_TOKEN_URL + params)
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

Plugin.install = function (Vue) {
  Vue.prototype.axios = _axios
  Vue.prototype.$authlogin = login
  Vue.prototype.$authlogout = logout
  window.axios = _axios
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
