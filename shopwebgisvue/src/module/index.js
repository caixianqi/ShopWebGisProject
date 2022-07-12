const Menu = (resolve) => require(['@/module/Menu'], resolve)

export default [
  {
    path: '/menu',
    name: 'menu',
    meta: {
      keepAlive: true,
      title: ['菜单管理', '菜单'],
    },
    component: Menu,
  },
]
