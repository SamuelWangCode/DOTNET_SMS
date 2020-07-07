<template>
<div>
<nav-menu :activeName="'1-2'" :openNames="['1']"></nav-menu>
<div class="content">
    <div style="padding-top:100px;">
  <Form ref="form" :model="form" :rules="rule" class="form">
      <FormItem prop="oldPassword">
      <Input type="password" v-model="form.oldPassword" placeholder="旧密码">
        <Icon type="ios-lock-outline" slot="prepend"></Icon>
      </Input>
    </FormItem>
    <FormItem prop="newPassword">
      <Input type="password" v-model="form.newPassword" placeholder="新密码">
        <Icon type="ios-lock-outline" slot="prepend"></Icon>
      </Input>
    </FormItem>
    <FormItem>
      <Button type="primary" @click="submit()">提交</Button>
    </FormItem>
    </Form>
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
  name: 'Password',
  components:{
    navMenu
  },
  data() {
    return {
      form: {
        oldPassword: "",
        newPassword: ""
      },
      rule: {
        oldPassword: [{ required: true, message: "请输入密码", trigger: "blur" },
          {
            type: "string",
            min: 5,
            message: "密码不应该短于5位",
            trigger: "blur" }],
        newPassword: [
          { required: true, message: "请输入密码", trigger: "blur" },
          {
            type: "string",
            min: 5,
            message: "密码不应该短于5位",
            trigger: "blur"
          }
        ]
      }
    }
  },
  methods:{
      submit: function(){
          let data = {
              Id: this.$store.state.Id,
              oldPassword: this.form.oldPassword,
              newPassword: this.form.newPassword
          }
          console.log(data)
          this.changePassword(data).then(Response =>{
              if(Response.data.code==200){
                  this.$Notice.success({
                    title: "密码更改成功",
                    desc: ""
                    });
              }else{
                  this.$Notice.error({
                    title: Response.data.message,
                    desc: ""
                    });
              }
          })
      }
  }
}
</script>
