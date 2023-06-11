const Menu = (resolve) => require(['@/module/system/Menu'], resolve)
const Home = (resolve) => require(['@/components/Home'], resolve)
const DataDictionary = (resolve) =>
  require(['@/module/system/DataDictionary'], resolve)
export default [
  {
    path: '/home',
    component: Home,
    children: [
      {
        name: 'menu',
        path: 'menu',
        component: Menu,
        meta: {
          title: ['菜单管理', '菜单'],
        },
      },
      {
        name: 'dataDictionary',
        path: 'dataDictionary',
        component: DataDictionary,
        meta: {
          title: ['菜单管理', '数据字典'],
        },
      },
    ],
  },
]
