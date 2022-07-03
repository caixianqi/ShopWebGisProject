const Menu = (resolve) => require(['@/module/Menu'], resolve)

export default [
  {
    path: '/menu',
    name: 'menu',
    meta: {
      keepAlive: true,
      allowAnonymous: true,
      title: '菜单',
    },
    component: Menu,
  },
]
