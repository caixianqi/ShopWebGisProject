import Vue from 'vue'
import Vuex from 'vuex'
import { state } from './state.js'
import * as mutations from './mutations.js'
import plugins from './plugin.js'

Vue.use(Vuex)

export default new Vuex.Store({
  state,
  mutations,
  plugins,
  actions: {},
  modules: {},
})
