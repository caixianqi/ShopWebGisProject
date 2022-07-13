const Menu = (resolve) => require(['@/module/Menu'], resolve)
const Home = (resolve) => require(['@/components/Home'], resolve)

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
    ],
  },
]
