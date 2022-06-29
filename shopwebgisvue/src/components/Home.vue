<template>
  <el-container
    class="home_container"
    v-loading.fullscreen.lock="fullscreenLoading"
  >
    <el-header>
      <div>
        <img src="@/assets/img/shop.jpg" />
        <span>Portal</span>
      </div>
      <el-button type="info" @click="logout()">退出</el-button>
    </el-header>
    <el-container>
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
          default-active="2"
          class="el-menu-vertical-demo"
          background-color="#545c64"
          text-color="#fff"
          active-text-color="#409eff"
          :unique-opened="true"
          :collapse-transition="false"
          :collapse="isCollapse"
          :router="true"
        >
          <el-submenu
            :index="item.MenuId"
            v-for="item in menulist"
            :key="item.MenuId"
          >
            <template slot="title">
              <i class="el-icon-location"></i>
              <span>{{ item.MenuDesc }}</span>
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
        .get('/api/PortalMenu/UserMenu/Web')
        .then((res) => {
          that.menulist = res
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
  background-color: #696969;
  display: flex;
  justify-content: space-between;
  align-items: center;
  color: #ffffff;
  font-size: 20px;
  img {
    width: 50px;
    border-radius: 5px;
  }
  > div {
    display: flex;
    align-items: center;
    span {
      margin-left: 15px;
    }
  }
}
.el-aside {
  background-color: rgb(84, 92, 100);
  .el-menu {
    border-right: none;
    word-wrap: break-word;
  }
}
.el-footer {
  background-color: #696969;
}
.el-main {
  background-color: #f5f5f5;
}
.el-icon-menu {
  margin-right: 10px;
}
.el-menu-item {
  span {
    word-wrap: break-word;
  }
}
.collapseButton {
  width: 100%;
}
</style>
>
