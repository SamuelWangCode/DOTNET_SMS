import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    Id:-1,
    role:''
  },
  mutations: {
    edit(state, payload){state.Id = payload.Id; state.role=payload.role;}
  },
  actions: {
  },
  modules: {
  }
})
