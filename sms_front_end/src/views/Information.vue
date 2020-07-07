<template>
<div>
<nav-menu :activeName="'1-1'" :openNames="['1']"></nav-menu>
<div class="content">
  <div v-if="adminOnly">您好，您是本系统管理员</div>
  <div class="info" v-if="adminNot">
    <div class="oneInfo">
    <span>姓名</span>
    <Input v-model="name" size="small" class="input" disabled/>
    </div>
    <div class="oneInfo">
    <span>邮箱</span>
    <Input v-model="email" class="input"/>
    </div>
    <div class="oneInfo">
    <span>电话</span>
    <Input v-model="tel" class="input"/>
    </div>
    <div class="oneInfo">
    <span>学/工号</span>
    <Input v-model="Id"  class="input" disabled/>
    </div>
    <div class="oneInfo">
    <span>学院</span>
    <Input v-model="dep"  class="input" disabled/>
    </div>
    <div class="oneInfo">
    <span>身份</span>
    <Input v-model="role" class="input" disabled/>
    </div>
    <div class="oneInfo" v-if="studentOnly" >
    <span>年级</span>
    <Input v-model="grade" class="input" disabled/>
    </div>
    <div class="oneInfo" v-if="teacherOnly">
    <span>职称</span>
    <Input v-model="jobTitle" class="input" disabled/>
    </div>
    <Button @click="submit()" style="margin-top:20px;">提交</Button>
  </div>
</div>
</div>
</template>
<style scoped>
.content{
  margin-left:30%;
  width:50%
}
.oneInfo{
  display: inline;

}
span{
  float: left;
}
.input{
  float: left;
}
</style>
<script>
import navMenu from '../components/Menu'
export default {
  name: 'Information',
  data(){
    return{
      adminNot:true,
      adminOnly:false,
      name:"",
      Id:"",
      email:"",
      tel:"",
      dep:"",
      role:"",
      dep_id:"",
      password:"",
      jobTitle:"",
      grade:"",
      studentOnly:true,
      teacherOnly:false
    }
  },
  components:{
    navMenu
  },
  mounted(){
    if(this.$store.state.role=="admin"){
      this.adminOnly = true
      this.adminNot = false
    }
    else if(this.$store.state.role=="student"){
      this.studentOnly = true
      this.teacherOnly = false
    }
    else if(this.$store.state.role=="teacher"){
      this.studentOnly = false
      this.teacherOnly = true
    }
    let data = { Id : this.$store.state.Id}
    this.Id = this.$store.state.Id
        this.getInfoById(data).then(Response => {
          console.log(Response)
          if (Response.data.code == 200) {
            this.Id = Response.data.data.Id
            this.name = Response.data.data.name
            this.email = Response.data.data.email
            this.tel = Response.data.data.tel
            this.password = Response.data.data.password
            this.dep_id = Response.data.data.dep_id
            if(Response.data.data.role=="student"){
              this.role = "学生"
            }
            else if(Response.data.data.role=="teacher"){
              this.role = "教师"
            }
            this.dep = Response.data.data.dep_name
            if(Response.data.data.role=="student"){
              this.grade = Response.data.data.grade
            }else{
              this.jobTitle = Response.data.data.job_title
            }
          } else {
            this.$Notice.error({
              title: Response.data.message,
              desc: ""
            });
        } 
        })
  },
  methods:{
    submit: function(){
      let data={
        Id: this.Id,
        name: this.name,
        password: this.password,
        email: this.email,
        tel: this.tel,
        dep_id: this.dep_id,
        role:this.$store.state.role
      }
      this.changeInformation(data).then(Response=>{
        if (Response.data.code == 200) {
          this.$Notice.success({
              title: "更改成功",
              desc: ""
            });
        }else {
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
