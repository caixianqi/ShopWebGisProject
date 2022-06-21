export const UPDATE_USER = (state, user) => {
  state.user = user
}

export const UPDATE_AUTH = (state, auth) => {
  state.auth = auth
}

export const CLEAR_ALL_DATA = (state) => {
  // Auth
  state.auth.isLoggedIn = false
  state.auth.accessToken = null
  state.auth.refreshToken = null

  // User
  state.user.userId = ''
  state.user.userName = ''
  state.user.nickName = ''
  state.user.loginType = ''
  state.user.departName = ''
  state.user.departId = ''
  state.user.avatarUrl = ''
  state.user.userType = ''
}
