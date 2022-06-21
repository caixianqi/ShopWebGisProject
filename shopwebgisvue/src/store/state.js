export const STORAGE_KEY = 'ShopVueStorage'

let syncedData = {
  auth: {
    isLoggedIn: false,
    accessToken: '',
    refreshToken: null,
  },
  user: {
    userId: null,
    userName: null,
    nickName: null,
    loginType: null,
    avatarUrl: null,
    departName: null,
    departId: null,
    departType: null,
  },
}

if (localStorage.getItem(STORAGE_KEY)) {
  syncedData = JSON.parse(localStorage.getItem(STORAGE_KEY))
}

export const state = syncedData
