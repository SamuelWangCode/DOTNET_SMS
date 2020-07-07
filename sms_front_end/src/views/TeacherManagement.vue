<template>
<div>
<nav-menu :activeName="'3-2'" :openNames="['3']"></nav-menu>
<div class="content">
  <div v-if="adminNot">
        您没有权限查看
    </div>
  <div v-if="adminOnly">
      <Table class="info" :columns="columns" :data="data">
        
    </Table>
    <Button @click="addStudent()">添加教师</Button>
  <Modal v-model="showModal" @on-ok="submit()"><div class="oneInfo">
    <span>姓名</span>
    <Input v-model="name" class="input"/>
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
    <Input v-model="Id"  class="input" />
    </div>
    <div class="oneInfo">
    <span>学院</span>
    <Input v-model="dep"  class="input"/>
    </div>
    <div class="oneInfo">
    <span>职称</span>
    <Input v-model="jobTitle" class="input"/>
    </div></Modal>
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
  name: 'TeacherManagement',
  components:{
    navMenu
  },
  data(){
      return{
          name:"",
          email:"",
          tel:"",
          Id:"",
          dep:"",
          jobTitle:"",
          showModal:false,
          adminOnly:false,
          adminNot:true,
          columns: [
                    {
                        title: '工号',
                        key: 't_id'
                    },
                    {
                        title: '姓名',
                        key: 't_name'
                    },
                    {
                        title: '学院',
                        key: 'dep_name'
                    },
                    {
                        title: '职称',
                        key: 'job_title'
                    },
                    {
                        title: '邮箱',
                        key: 'email'
                    },
                    {
                        title: '电话',
                        key: 'tel'
                    },
                ],
                data: [
                ]
      }
  },
  mounted(){
      if(this.$store.state.role=="admin"){
          this.adminOnly = true
          this.adminNot = false
      }
      this.getAllTeachers().then(Response=>{
          if(Response.data.code==200){
              this.data = Response.data.data
          }else{
              this.$Notice.error({
                    title: Response.data.message,
                    desc: ""
                    });
          }
      })
  },
  methods:{
      addStudent : function(){
          this.showModal = true
      },
      submit : function(){
          var dep_id = 1
          if(this.dep=="软件学院"){
              dep_id = 1
          }else if(this.dep=="电信学院"){
              dep_id = 2
          }
          else if(this.dep=="建筑学院"){
              dep_id = 3
          }
          else if(this.dep=="土木学院"){
              dep_id = 4
          }
          else if(this.dep=="文学院"){
              dep_id = 5
          }
          else if(this.dep=="经济学院"){
              dep_id = 6
          }
          else if(this.dep=="生命科学与技术学院"){
              dep_id = 7
          }
          let data = {
              Id: this.Id,
              name: this.name,
              email: this.email,
              tel: this.tel,
              password: this.Id,
              dep_id: dep_id,
              job_title: this.jobTitle
          }
          this.postTeacher(data).then(Response=>{
              if(Response.data.code==200){
                  this.$Notice.success({
                    title: "成功",
                    desc: ""
                    });
                    this.$router.go(0);
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
