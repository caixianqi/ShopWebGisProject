<template>
  <div
    class="login_container"
    v-loading.fullscreen.lock="fullscreenLoading"
    :element-loading-text="$t('loading')"
  >
    <div class="login_box">
      <div class="avatar_box">
        <img src="../../assets/img/logo.png" />
      </div>
      <el-form
        ref="form"
        :model="form"
        class="login_form"
        :rules="loginFormRules"
      >
        <el-form-item prop="name">
          <el-input v-model="form.name" placeholder="用户名"></el-input>
        </el-form-item>
        <el-form-item prop="password">
          <el-input
            v-model="form.password"
            type="password"
            placeholder="密码"
          ></el-input>
        </el-form-item>
        <el-form-item class="btns">
          <el-button type="primary" @click="Prelogin('form')">{{
            $t('login')
          }}</el-button>
          <el-button type="info" @click="reset">{{ $t('reset') }}</el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script>
import Crypto from 'jsencrypt'
export default {
  data() {
    //自定义验证用户名
    var validatname = (rule, value, callback) => {
      if (!value.replace(/[^\w]/gi, '')) {
        callback(
          new Error(this.$t('Prohibit input except English and numbers'))
        )
      } else {
        callback()
      }
    }
    return {
      fullscreenLoading: false,
      form: {
        name: '',
        password: '',
      },
      loginFormRules: {
        name: [
          { required: true, message: '请输入用户名', trigger: 'blur' },
          { validator: validatname, trigger: 'blur' },
        ],
        password: [{ required: true, message: '请输入密码', trigger: 'blur' }],
      },
    }
  },
  created() {
    var auth = this.$store.state.auth
    if (auth.isLoggedIn) {
      this.$router.push({
        path: '/home',
      })
      return
    }
  },
  methods: {
    ///登录前验证
    Prelogin(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.login()
        } else {
          this.$message.error(this.$t('form_validate'))
          return false
        }
      })
    },
    reset: function () {
      this.$refs.form.resetFields()
      this.$message.success('重置成功！')
    },
    //登录
    login() {
      this.fullscreenLoading = true
      this.axios
        .get('/User/GetCryptoPublicKey')
        .then((publicKey) => {
          var encrypt = new Crypto()
          encrypt.setPublicKey(publicKey)
          console.log('encrypt', encrypt)
          var creds = {
            username: this.form.name,
            password: encodeURIComponent(encrypt.encrypt(this.form.password)),
          }
          return this.$authlogin(creds)
        })
        .then(() => {
          return this.axios('/User/GetUserInfo')
        })
        .then((res) => {
          debugger
          const user = {
            userId: res.id,
            userName: res.name,
          }
          this.$store.commit('UPDATE_USER', user)
          this.gotomainview()
        })
        .catch((error) => {
          if (error.data) {
            this.$message.error(error.data.error_description)
          } else {
            this.$message.error(error)
          }
        })
      this.fullscreenLoading = false
    },
    gotomainview() {
      this.$router.push({
        path: this.$route.query.redirect || '/home',
      })
    },
  },
}
</script>

<style lang="less" scoped>
.login_container {
  background-color: #2b4b6b;
  height: 100%;
}

.login_box {
  width: 450px;
  height: 300px;
  background-color: #fff;
  border-radius: 3px;
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);

  .avatar_box {
    height: 130px;
    width: 130px;
    border: 1px solid #eee;
    border-radius: 50%;
    padding: 10px;
    box-shadow: 0 0 10px #ddd;
    position: absolute;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: #fff;
    img {
      width: 100%;
      height: 100%;
      border-radius: 50%;
      background-color: #eee;
    }
  }

  .login_form {
    position: absolute;
    bottom: 0;
    width: 100%;
    padding: 0 20px;
    box-sizing: border-box;
  }

  .btns {
    display: flex;
    justify-content: flex-end;
  }
}
</style>
