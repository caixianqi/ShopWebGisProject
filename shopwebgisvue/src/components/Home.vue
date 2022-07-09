<template>
  <el-container
    class="home_container"
    v-loading.fullscreen.lock="fullscreenLoading"
  >
    <el-aside :width="!isCollapse ? '200px' : '64px'">
      <el-button
        type="info"
        plain
        class="collapseButton"
        :autofocus="isCollapse"
        @click="toggleCollapse"
        >{{ !isCollapse ? '收起' : '展开' }}</el-button
      >
      <el-menu
        default-active="home"
        background-color="#324057"
        text-color="#bfcbd9"
        active-text-color="#20a0ff"
        :unique-opened="true"
        :collapse-transition="false"
        :collapse="isCollapse"
        :router="true"
      >
        <el-menu-item index="home"
          ><i class="el-icon-menu"></i>首页</el-menu-item
        >
        <el-submenu :index="item.id" v-for="item in menulist" :key="item.id">
          <template slot="title">
            <i class="el-icon-document"></i>
            <span>{{ item.name }}</span>
          </template>
          <el-menu-item
            :index="'/' + subItem.FormUrl"
            v-for="subItem in item.SubMenu"
            :key="subItem.MenuId"
          >
            <i class="el-icon-menu"></i>
            <span slot="title">{{ subItem.MenuDesc }}</span>
          </el-menu-item>
        </el-submenu>
      </el-menu>
    </el-aside>
    <el-container>
      <el-header>
        <header-top></header-top>
      </el-header>
      <el-main>
        <keep-alive>
          <router-view v-if="$route.meta.keepAlive"></router-view>
        </keep-alive>
        <router-view v-if="!$route.meta.keepAlive"></router-view>
      </el-main>
    </el-container>
  </el-container>
</template>

<script>
export default {
  data() {
    return {
      menulist: [],
      fullscreenLoading: false,
      isCollapse: false,
      issuccess: false,
    }
  },
  //写一个钩子，从登录页面进来的提示登录成功！
  beforeRouteEnter(to, from, next) {
    next((vm) => {
      if (from.name && from.name.indexOf('登录')) {
        vm.issuccess = true
      }
    })
  },
  created() {
    this.getMenuList()
  },
  mounted() {
    if (this.issuccess) {
      this.$message.success('登录成功！')
    }
  },
  methods: {
    logout() {
      this.$authlogout()
    },
    getMenuList() {
      this.fullscreenLoading = true
      var that = this
      this.axios
        .get('/Menu/GetMenuList')
        .then((res) => {
          that.menulist = res
          that.menulist.forEach((x) => (x.id = x.id.toString()))
        })
        .catch((error) => {
          this.$message.error(error)
        })
      this.fullscreenLoading = false
    },
    toggleCollapse() {
      this.isCollapse = !this.isCollapse
    },
  },
}
</script>

<style lang="less" scoped>
.home_container {
  height: 100%;
}
// .logout {
//   float: right;
//   top: 10px;
//   position: relative;
// }
.el-header {
  padding: 0 0;
}
.el-aside {
  background-color: #324057;
}
.el-main {
  background-color: #f5f5f5;
}
/deep/ .el-submenu__title {
  padding: 0 150px;
}
/deep/ .el-menu-item {
  padding: 0 150px;
}
.collapseButton {
  width: 100%;
}
</style>
>
