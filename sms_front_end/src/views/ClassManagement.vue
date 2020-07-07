<template>
<div>
<nav-menu :activeName="'3-3'" :openNames="['3']"></nav-menu>
<div class="content">
    <div v-if="adminNot">
        您没有权限查看
    </div>
  <div v-if="adminOnly">
      <Table class="info" :columns="columns" :data="data">
        
    </Table>
    <Button @click="addStudent()">添加班级</Button>
    <Button @click="addClass()">加入课程</Button>
  <Modal v-model="showModal" @on-ok="submit()"><div class="oneInfo">
    <span>课程名</span>
    <Input v-model="courseName" class="input"/>
    </div>
    <div class="oneInfo">
    <span>学院</span>
    <Input v-model="dep" class="input"/>
    </div>
    <div class="oneInfo">
    <span>任课教师工号</span>
    <Input v-model="t_id" class="input"/>
    </div>
    <div class="oneInfo">
    <span>学分</span>
    <Input v-model="credits"  class="input" />
    </div>
    </Modal>
    <Modal v-model="showModal2" @on-ok="submit1()"><div class="oneInfo">
    <span>课程号</span>
    <Input v-model="courseId" class="input"/>
    </div>
    <div class="oneInfo">
    <span>学号</span>
    <Input v-model="sId" class="input"/>
    </div>
    </Modal>
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
  name: 'ClassManagement',
  components:{
    navMenu
  },
  data(){
      return{
          courseId:"",
          sId:"",
          courseName:"",
          dep:"",
          t_id:"",
          credits:"",
          showModal:false,
          showModal2:false,
          adminOnly:false,
          adminNot:true,
           columns: [
                    {
                        title: '班号',
                        key: 'course_id'
                    },
                    {
                        title: '课程名称',
                        key: 'course_name'
                    },
                    {
                        title: '学院',
                        key: 'dep_name'
                    },
                    {
                        title: '教师',
                        key: 't_name'
                    },
                    {
                        title: '学分',
                        key: 'credits'
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
      this.getAllClasses().then(Response=>{
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
      addClass : function(){
          this.showModal2 = true
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
              course_name: this.courseName,
              dep_id: dep_id,
              t_id: this.t_id,
              credits: this.credits
          }
          this.postClass(data).then(Response=>{
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
      },
      submit1 : function(){
          let data = {
              course_id: this.courseId,
              s_id: this.sId
          }
          this.postStudentClass(data).then(Response=>{
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
