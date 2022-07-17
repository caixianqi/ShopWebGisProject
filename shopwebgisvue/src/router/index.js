import Vue from 'vue'
import VueRouter from 'vue-router'
import ModuleRouter from '../module/index'

const originalPush = VueRouter.prototype.push
const originalReplace = VueRouter.prototype.replace
// push
VueRouter.prototype.push = function push(location) {
  // if (onResolve || onReject) return originalPush.call(this, location, onResolve, onReject)
  return originalPush.call(this, location).catch((err) => {
    err
  })
}
// replace
VueRouter.prototype.replace = function replace(location) {
  // if (onResolve || onReject) return originalReplace.call(this, location, onResolve, onReject)
  return originalReplace.call(this, location).catch((err) => err)
}

Vue.use(VueRouter)
const Login = (resolve) => require(['@/components/User/Login'], resolve)
const Home = (resolve) => require(['@/components/Home'], resolve)
//const Welcome = (resolve) => require(['@/components/Welcome'], resolve)
// const Register = (resolve) => require(['@/components/login/Register'], resolve)
const router = new VueRouter({
  routes: [
    {
      path: '/',
      redirect: {
        name: 'login',
      },
    },
    {
      path: '/login',
      name: 'login',
      meta: {
        keepAlive: true,
        allowAnonymous: true,
        title: '登录',
      },
      component: Login,
    },
    // {
    //   path: '/register',Welcome
    //   name: 'register',
    //   meta: {
    //     keepAlive: true,
    //     allowAnonymous: true,
    //     title: '注册',
    //   },
    //   component: Register,
    // },
    {
      path: '/home',
      name: 'home',
      meta: {
        keepAlive: false,
        title: ['主页'],
        allowAnonymous: false,
      },
      component: Home,
      // redirect: {
      //   name: 'welcome',
      // },
      // children: [
      //   {
      //     path: 'welcome',
      //     name: 'welcome',
      //     component: Welcome,
      //   },
      // ],
    },
    ...ProductRouterFromModule(),
  ],
})

function ProductRouterFromModule() {
  return ModuleRouter
}

// const originalPush = VueRouter.prototype.push
// const originalReplace = VueRouter.prototype.replace
// // push
// VueRouter.prototype.push = function push (location, onResolve, onReject) {
//   if (onResolve || onReject) return originalPush.call(this, location, onResolve, onReject)
//   return originalPush.call(this, location).catch(err => err)
// }
// // replace
// VueRouter.prototype.replace = function push (location, onResolve, onReject) {
//   if (onResolve || onReject) return originalReplace.call(this, location, onResolve, onReject)
//   return originalReplace.call(this, location).catch(err => err)
// }

// 路由守卫
router.beforeEach((to, from, next) => {
  if (to.meta.allowAnonymous) {
    return next()
  }
  const auth = router.app.$options.store.state.auth
  if (!auth.isLoggedIn) {
    if (to.name === 'login') {
      next()
    } else {
      next({
        name: 'login',
      })
    }
  } else {
    next()
  }
  /* 路由发生变化修改页面title */
  if (to.meta.title) {
    document.title = to.meta.title
  }
})

export default router
