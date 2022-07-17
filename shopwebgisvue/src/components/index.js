import HeaderTop from './HeaderTop.vue'
import TableHeader from './Common/TableHeader.vue'

const components = [HeaderTop, TableHeader]

export default {
  install(Vue) {
    components.forEach((component) => {
      debugger
      // 注册全局组件
      Vue.component(component.name, component)
    })
  },
}
