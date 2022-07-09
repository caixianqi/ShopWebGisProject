<template>
  <div class="header_container">
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/home' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item
        v-for="(item, index) in $route.meta.title"
        :key="index"
        >{{ item }}</el-breadcrumb-item
      >
    </el-breadcrumb>
    <el-dropdown @command="handleCommand">
      <img src="@/assets/img/default.jpg" class="avator" />
      <el-dropdown-menu slot="dropdown">
        <el-dropdown-item command="home">首页</el-dropdown-item>
        <el-dropdown-item command="updateUser">修改用户信息</el-dropdown-item>
        <el-dropdown-item command="logout">退出</el-dropdown-item>
      </el-dropdown-menu>
    </el-dropdown>
  </div>
</template>
<script>
export default {
  data() {
    return {}
  },
  mounted() {},
  methods: {
    handleCommand(command) {
      if (command === 'home') {
        this.$router.push('/home')
      } else if (command === 'logout') {
        this.$authlogout()
      } else {
        this.$message.error('暂无提供修改用户信息界面,请勿点击!')
      }
    },
  },
}
</script>
<style lang="less" scoped>
.header_container {
  display: flex;
  justify-content: space-between;
  background-color: #eff2f7;
  height: 60px;
  align-items: center;
  padding-left: 20px;
}
.avator {
  border-radius: 50%;
  height: 36px;
  width: 40px;
  margin-right: 40px;
}
</style>
