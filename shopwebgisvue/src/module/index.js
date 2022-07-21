const Menu = (resolve) => require(['@/module/system/Menu'], resolve)
const Home = (resolve) => require(['@/components/Home'], resolve)
const CommodityManagement = (resolve) =>
  require(['@/module/goods/commoditymanagement'], resolve)
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
        name: 'commoditymanagement',
        path: 'commoditymanagement',
        component: CommodityManagement,
        meta: {
          title: ['商品管理', '商品分类'],
        },
      },
    ],
  },
]
