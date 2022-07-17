import Vue from 'vue'
import '@/plugin/axios.js'
import App from './App.vue'
import '@/plugin/elementui.js'
import i18n from './i18n'
import CommonComponent from './packages'
import router from './router'
import store from './store'
import '@/assets/css/global.css'
import components from './components'
import './assets/icon/iconfont.css'
import * as echarts from 'echarts'
Vue.prototype.$echarts = echarts
Vue.use(components)
Vue.use(CommonComponent)
Vue.config.productionTip = false

new Vue({
  router,
  store,
  i18n,
  render: (h) => h(App),
}).$mount('#app')
