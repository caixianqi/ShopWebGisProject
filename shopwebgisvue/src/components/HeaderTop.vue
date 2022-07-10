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
      <el-row>
        <el-col :span="10"
          ><img src="@/assets/img/default.jpg" class="avator"
        /></el-col>
        <el-col :span="8" style="margin-top: 12px"
          ><span>{{ $store.state.user.userName }}</span></el-col
        >
      </el-row>

      <el-dropdown-menu slot="dropdown">
        <el-dropdown-item command="home">首页</el-dropdown-item>
        <el-dropdown-item command="updateUser">修改密码</el-dropdown-item>
        <el-dropdown-item command="logout">退出</el-dropdown-item>
      </el-dropdown-menu>
    </el-dropdown>
    <el-dialog
      title="用户信息"
      :visible.sync="dialogVisible"
      width="40%"
      v-loading="loading"
    >
      <el-form
        ref="form"
        :model="form"
        label-width="120px"
        label-suffix="："
        :rules="rules"
        label-position="left"
      >
        <el-form-item label="用户名" required>
          <el-input v-model="form.UserName" :disabled="true"></el-input>
        </el-form-item>
        <el-form-item label="原始密码" prop="OriginalPassword" required
          ><el-input
            v-model="form.OriginalPassword"
            type="password"
            show-password
          ></el-input>
        </el-form-item>
        <el-form-item label="新密码" prop="NewPassword" required>
          <el-input
            v-model="form.NewPassword"
            type="password"
            show-password
          ></el-input
        ></el-form-item>
        <el-form-item label="确认新密码" prop="CheckPassword" required>
          <el-input
            v-model="form.CheckPassword"
            type="password"
            show-password
          ></el-input
        ></el-form-item>
        <el-form-item class="btn">
          <el-button type="primary" @click="UserModify('form')">修改</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </div>
</template>
<script>
import Crypto from 'jsencrypt'
export default {
  data() {
    return {
      loading: false,
      dialogVisible: false,
      form: {
        UserName: '',
        NewPassword: '',
        OriginalPassword: '',
        CheckPassword: '',
      },
      rules: {
        OriginalPassword: [
          { required: true, message: '请填写原始密码', trigger: 'blur' },
        ],
        NewPassword: [
          { required: true, message: '请填写新密码', trigger: 'blur' },
        ],
      },
    }
  },
  mounted() {
    if (this.$store.state.user) {
      this.form.Id = this.$store.state.user.userId
      this.form.UserName = this.$store.state.user.userName
    }
  },
  methods: {
    handleCommand(command) {
      if (command === 'home') {
        this.$router.push('/home')
      } else if (command === 'logout') {
        this.$authlogout()
      } else {
        this.dialogVisible = true
      }
    },
    UserModify(FormName) {
      this.$refs[FormName].validate((valid) => {
        if (valid) {
          if (!this.CheckUserPassword()) {
            this.$message.error('两次密码输入不一致，请再次输入!')
            return
          }
          this.loading = true
          this.axios
            .get('/User/GetCryptoPublicKey')
            .then((publicKey) => {
              var encrypt = new Crypto()
              encrypt.setPublicKey(publicKey)
              const param = {
                OriginalPassword: encrypt.encrypt(this.form.OriginalPassword),
                UserPassword: encrypt.encrypt(this.form.NewPassword),
              }
              debugger
              return this.axios.put('/User/ModifyUser', param)
            })
            .then(() => {
              this.$message.success('修改成功,请重新登录!')
              this.$authlogout()
            })
            .catch((error) => {
              this.$message.error(error)
            })
            .finally(() => {
              this.loading = false
              this.$refs[FormName].resetFields()
              this.dialogVisible = false
            })
        } else {
          this.$message.error(this.$t('form_validate'))
          return false
        }
      })
    },
    CheckUserPassword() {
      var ispass = false
      if (this.form.CheckPassword === this.form.NewPassword) {
        ispass = true
      }
      return ispass
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
.btn {
  display: flex;
  justify-content: flex-end;
}
</style>
