<template>
  <div
    class="login_container"
    v-loading.fullscreen.lock="fullscreenLoading"
    :element-loading-text="$t('loading')"
  >
    <div class="login_box">
      <div class="login_title"><p>Login</p></div>

      <el-form
        ref="form"
        :model="form"
        class="login_form"
        :rules="loginFormRules"
      >
        <el-form-item prop="name">
          <el-input
            v-model="form.name"
            placeholder="用户名"
            prefix-icon="el-icon-user-solid"
          ></el-input>
        </el-form-item>
        <el-form-item prop="password">
          <el-input
            prefix-icon="iconfont icon-mima"
            v-model="form.password"
            type="password"
            placeholder="密码"
            show-password
            @keyup.enter.native="Prelogin('form')"
          ></el-input>
        </el-form-item>
        <el-form-item class="btns">
          <el-button @click="Prelogin('form')">
            {{ $t('login') }}
          </el-button>
          <el-button @click="reset">{{ $t('reset') }}</el-button>
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
  mounted() {
    this.renderLoginHoverEffect()
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
        .finally(() => {
          this.fullscreenLoading = false
        })
    },
    gotomainview() {
      this.$router.push({
        path: this.$route.query.redirect || '/home',
      })
    },
    renderLoginHoverEffect() {
      //获取 login
      let login = document.querySelector('.login_box')
      let span
      let inTime, outTime
      let isIn = true //默认开关 打开
      let isOut

      //鼠标进入事件
      login.addEventListener('mouseenter', function (e) {
        isOut = false //预先关闭，若不进入if语句，则不能进入鼠标离开事件里的 if
        if (isIn) {
          inTime = new Date().getTime()

          //生成 span 元素并添加进 login 的末尾
          span = document.createElement('span')
          span.classList.add('loginanimationspan')
          login.appendChild(span)
          //span 去使用 in动画
          span.style.animation = 'in .5s ease-out forwards'
          console.log(e)
          //计算 top 和 left 值，跟踪鼠标位置
          let top = e.clientY - e.target.offsetTop
          let left = e.clientX - e.target.offsetLeft

          span.style.top = top + 'px'
          span.style.left = left + 'px'
          isIn = false //当我们执行完程序后，关闭
          isOut = true //当我们执行完里面的程序，再打开
        }
      })
      //鼠标离开事件
      login.addEventListener('mouseleave', function (e) {
        if (isOut) {
          outTime = new Date().getTime()
          let passTime = outTime - inTime

          if (passTime < 500) {
            setTimeout(mouseleave, 500 - passTime) //已经经过的时间就不要了
          } else {
            mouseleave()
          }
        }

        function mouseleave() {
          span.style.animation = 'out .5s ease-out forwards'

          //计算 top 和 left 值，跟踪鼠标位置
          let top = e.clientY - e.target.offsetTop
          let left = e.clientX - e.target.offsetLeft

          span.style.top = top + 'px'
          span.style.left = left + 'px'

          //注意：因为要等到动画结束，所以要给个定时器
          setTimeout(function () {
            login.removeChild(span)
            isIn = true //当我们执行完鼠标离开事件里的程序，才再次打开
          }, 500)
        }
      })
    },
  },
}
</script>

<style lang="less" scoped>
.login_container {
  background-color: #2c3e50;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.login_box {
  overflow: hidden;
  width: 320px;
  height: 430px;
  background-color: #34495e;
  position: absolute;
  border-radius: 10px;
  box-shadow: 10px 10px 20px rgba(33, 44, 55, 0.3);

  .login_form {
    position: absolute;
    bottom: 0;
    width: 100%;
    padding: 0 20px;
    box-sizing: border-box;
    margin-bottom: 50px;
    z-index: 1;
  }
  .btns {
    display: flex;
    justify-content: flex-end;
  }
}
.login_title {
  height: 130px;
  width: 100%;
  position: absolute;
  //transform: translate(-50%, -80%);
  p {
    font-size: 38px;
    color: #fff;
  }
  font-weight: 400;
  z-index: 1;
}
.el-button {
  background: transparent;
  color: #fff;
}
.el-button:hover {
  background: #2c3e50;
}
.el-form-item {
  margin-top: 35px;
}
/deep/ .el-input__inner {
  background-color: transparent;
  color: #fff;
}
/deep/ .el-input__inner:focus {
  border-color: #fff;
}
</style>
<style>
/* 鼠标进入 login 时的动画 */
@keyframes in {
  0% {
    width: 0;
    height: 0;
  }
  100% {
    width: 1200px;
    height: 1200px;
  }
}

/* 鼠标离开 login 时的动画 */
@keyframes out {
  0% {
    width: 1200px;
    height: 1200px;
  }
  100% {
    width: 0;
    height: 0;
  }
}
.loginanimationspan {
  position: absolute;
  top: 0;
  left: 0;
  width: 0;
  height: 0;
  background-color: #2e455c;
  /* 鼠标居中 */
  transform: translate(-50%, -50%);
  /* 圆的形式展开 */
  border-radius: 50%;
}
</style>
