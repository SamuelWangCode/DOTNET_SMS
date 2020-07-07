<template>
  <Form ref="form" :model="form" :rules="rule" class="form">
    <FormItem prop="user">
      <Input type="text" v-model="form.user" placeholder="学号/工号">
        <Icon type="ios-person-outline" slot="prepend"></Icon>
      </Input>
    </FormItem>
    <FormItem prop="password">
      <Input type="password" v-model="form.password" placeholder="密码">
        <Icon type="ios-lock-outline" slot="prepend"></Icon>
      </Input>
    </FormItem>
    <FormItem>
      <Button type="primary" @click="handleSubmit()">登录</Button>
    </FormItem>
  </Form>
</template>
<style scoped>
.form{
    width:50%;
    margin-top: 10%;
    margin-left: auto;
    margin-right: auto;
}
</style>
<script>
export default {
  name: "Login",
  data() {
    return {
      form: {
        user: "",
        password: ""
      },
      rule: {
        user: [{ required: true, message: "请输入学号", trigger: "blur" }],
        password: [
          { required: true, message: "请输入密码", trigger: "blur" },
          {
            type: "string",
            min: 5,
            message: "密码不应该短于5位",
            trigger: "blur"
          }
        ]
      }
    };
  },
  methods: {
    handleSubmit: function() {
        let data = {
          Id: this.form.user,
          password: this.form.password
        }
        console.log(data)
        this.login(data).then(Response => {
          console.log(Response)
          if (Response.data.code == 200) {
            this.$Notice.success({
              title: "登陆成功!",
              desc: ""
            });
            var Id = Response.data.data.Id;
            var role = Response.data.data.role;
            this.$store.commit('edit',{Id:Id, role:role})
            console.log("Id:" + this.$store.state.Id)
            console.log("role:" + this.$store.state.role)
            this.$router.push("/information");
          } else {
            this.$Notice.error({
              title: Response.data.message,
              desc: ""
            });
        } 
        })
    },

  },
};
</script>