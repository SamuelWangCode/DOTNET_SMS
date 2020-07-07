<template>
<div>
<nav-menu :activeName="'2-2'" :openNames="['2']"></nav-menu>
<div class="content">
  <div v-if="adminOnly">
        您没有权限查看
    </div>
  <div v-if="adminNot">
      <Table class="info" :columns="columns" :data="data" v-if="studentOnly">
        
    </Table>
    <Button v-if="studentOnly" @click="calGrade()">计算总绩点</Button>
    <div>{{final_grade}}</div>
    <Table class="info" :columns="columns2" :data="data2" v-if="teacherOnly">
        
    </Table>
    
  </div>
  <Modal v-model="showModal" title="打分"><Table class="info" :columns="columns3" :data="data3"></Table></Modal>
<Modal v-model="showModal2" title="输入" @on-ok="score()" ><Input v-model="value"></Input></Modal>
    
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
  name: 'Grade',
  components:{
    navMenu
  },
  data(){
      return{
          final_grade:"",
          course_id:"",
          value:0,
          index:"",
          showModal:false,
          showModal2:false,
          adminOnly:false,
          adminNot:true,
          teacherOnly:false,
          studentOnly:true,
          columns: [
                    {
                        title: '课程',
                        key: 'course_name'
                    },
                    {
                        title: '学分',
                        key: 'credits'
                    },
                    {
                        title: '成绩',
                        key: 'grade'
                    },
                ],
                data: [
                ],
        columns2: [
                    {
                        title: '课程',
                        key: 'course_name'
                    },
                    {
                        title: '动作',
                        key: 'action',
                        render: (h, params) => {
                            return h('div', [
                                h('Button', {
                                    props: {
                                        type: 'primary',
                                        size: 'small',
                                    },
                                    style: {
                                        marginRight: '5px'
                                    },
                                    on: {
                                        click: () => {
                                            this.show(params.index)
                                        }
                                    }
                                }, "打开")
                                ]);
                    },
                    }
                ],
                data2: [
                ],
                columns3: [
                    {
                        title: '学号',
                        key: 's_id'
                    },
                    {
                        title: '姓名',
                        key: 's_name'
                    },
                    {
                        title: '成绩',
                        key: 'grade'
                    },
                    {
                        title: '动作',
                        key: 'action',
                        render: (h, params) => {
                            return h('div', [
                                h('Button', {
                                    props: {
                                        type: 'primary',
                                        size: 'small',
                                    },
                                    style: {
                                        marginRight: '5px'
                                    },
                                    on: {
                                        click: () => {
                                            this.giveScore(params.index)
                                        }
                                    }
                                }, "打分")
                                ]);
                    },
                    }
                ],
                data3: [
                ]
      }
  },
  mounted(){
      if(this.$store.state.role=="admin"){
          this.adminOnly = true
          this.adminNot = false
      }
      if(this.$store.state.role=="teacher"){
          this.teacherOnly = true
          this.studentOnly = false
      }
      let data = {
          Id: this.$store.state.Id
      }
      this.findClassesById(data).then(Response =>{
          if(Response.data.code == 200){
              this.data = Response.data.data
              this.data2 = Response.data.data
          }
          else{
              this.$Notice.error({
                    title: Response.data.message,
                    desc: ""
                    });
          }
      })
  },
  methods:{
      show(index){
          this.showModal = true
        let data={
            course_id: this.data2[index].course_id
        }
        this.course_id = this.data2[index].course_id
        this.getStudentsByCourse(data).then(Response=>{
            if(Response.data.code==200){
                this.data3 = Response.data.data
            }
            else{
                this.$Notice.error({
                    title: Response.data.message,
                    desc: ""
                    });
            }
        })
      },
      calGrade(){
          var creditsArray = []
          var gradesArray = []
          for (var i =0;i<this.data.length;i++){
              creditsArray.push(this.data[i].credits)
          }
          for (var i =0;i<this.data.length;i++){
              if(this.data[i].grade!=null){
                gradesArray.push(this.data[i].grade)
              }
              else{
                  gradesArray.push(0)
              }
          }
          var sum1 = 0;
          var sum2 = 0;
          for(var i =0;i<this.data.length;i++){
              sum1 += creditsArray[i];
              sum2 += gradesArray[i]
          }
          this.final_grade = sum1/sum2;
      },
      giveScore(index){
          this.showModal2 = true;
        this.index = index
      },
      score: function(){
          var index = this.index
          let data={
                s_id: this.data3[index].s_id,
                course_id: this.course_id,
                grade: this.value
            }
            console.log(data)
            this.axios({ url: "http://localhost:56455/api/courses/giveScore", method: "post", data:data }).then(Response=>{
                if(Response.data.code==200){
                    this.$Notice.success({
                    title: "打分成功",
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
            // this.giveScore(data)
            // this.$Notice.success({
            //         title: "打分成功",
            //         desc: ""
            //         });
            // .then(Response=>{
            //     if(Response.data.code==200){
            //         this.$Notice.success({
            //         title: "打分成功",
            //         desc: ""
            //         });
            //         this.$router.go(0);
            //     }else{
            //         this.$Notice.error({
            //         title: Response.data.message,
            //         desc: ""
            //         });
            //     }
            // })
      }
  }
}
</script>
