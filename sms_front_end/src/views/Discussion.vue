<template>
<div>
<nav-menu :activeName="'2-3'" :openNames="['2']"></nav-menu>
<div class="content">
  <div v-if="adminOnly">
        您没有权限查看
    </div>
  <div v-if="adminNot">
      
  </div>
</div>
</div>
</template>
<style scoped>
.content{
  margin-left:30%;
  width:50%
}
</style>
<script>
import navMenu from '../components/Menu'
export default {
  name: 'Discussion',
  components:{
    navMenu
  },
  data(){
      return{
          adminOnly:false,
          adminNot:true
      }
  },
  mounted(){
      if(this.$store.state.role=="admin"){
          this.adminOnly = true
          this.adminNot = false
      }
  }
}
</script>
