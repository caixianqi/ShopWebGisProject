import HeaderTop from './HeaderTop.vue'
import TableHeader from './Common/TableHeader.vue'
import TableComponent from './Common/TableComponent.vue'

const components = [HeaderTop, TableHeader, TableComponent]

export default {
  install(Vue) {
    components.forEach((component) => {
      // 注册全局组件
      Vue.component(component.name, component)
    })
  },
}
