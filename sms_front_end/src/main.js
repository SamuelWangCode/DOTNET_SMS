import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import ViewUI from 'view-design';
import axios from 'axios'
// axios.defaults.withCredentials = true;
import VueAxios from 'vue-axios'
import 'view-design/dist/styles/iview.css';

Vue.config.productionTip = false

//写cookies
Vue.prototype.setCookie = function (name,value)
{
var Days = 30;
var exp = new Date();
exp.setTime(exp.getTime() + Days*24*60*60*1000);
document.cookie = name + "="+ escape (value) + ";expires=" + exp.toGMTString();
}
//读取cookies
Vue.prototype.getCookie = function (name)
{
var arr,reg=new RegExp("(^| )"+name+"=([^;]*)(;|$)");
if(arr=document.cookie.match(reg)) return unescape(arr[2]);
else return null;
}
//删除cookies
Vue.prototype.delCookie = function (name)
{
var exp = new Date();
exp.setTime(exp.getTime() - 1);
var cval=this.getCookie(name);
if(cval!=null) document.cookie= name + "="+cval+";expires="+exp.toGMTString();
}

//////////////////////////////////////////////////
function post(url, data){
  return axios({
    method: "POST",
    url: "http://localhost:56455/" + url,
    data: data,
  })
}
function get(url){
  return axios.get("http://localhost:56455/" + url);
}
function put(url, data){
  return axios({
    method: "PUT",
    url: "http://localhost:56455/" + url,
    data: data
  })
}
///////////////////////////////////////////
//USER
Vue.prototype.getInfoById = function (data){
  return post("api/users/GetUserById", data)
}
Vue.prototype.login = function (data){
  return post("api/users/Login", data);
}
Vue.prototype.changePassword = function (data){
  return post("api/users/ChangePassword", data);
}
Vue.prototype.changeInformation = function (data){
  return put("api/users/Putuser/" + data.Id, data);
}
Vue.prototype.findClassesById = function (data){
  return post("api/courses/FindClassesById" , data);
}
Vue.prototype.changeBroadCast = function (data){
  return post("api/courses/ChangeBroadCast" , data);
}
Vue.prototype.getStudentsByCourse = function (data){
  return post("api/courses/GetStudentsByCourse" , data);
}
Vue.prototype.giveScore = function (data){
  return post("api/courses/giveScore" , data);
}
Vue.prototype.getAllStudents = function (){
  return get("api/students/getAllStudents");
}
Vue.prototype.getAllTeachers = function (){
  return get("api/teachers/getAllTeachers");
}
Vue.prototype.getAllClasses = function (){
  return get("api/courses/getAllClasses");
}
Vue.prototype.postStudent = function (data){
  return post("api/students/PostStudent", data);
}
Vue.prototype.postTeacher = function (data){
  return post("api/teachers/PostTeacher", data);
}
Vue.prototype.postClass = function (data){
  return post("api/courses/PostClass", data);
}
Vue.prototype.postStudentClass = function (data){
  return post("api/courses/PostStudentClass", data);
}
////////////////////////////////////////

Vue.use(ViewUI)
Vue.use(VueAxios, axios)
router.beforeEach((to, from, next) => {
  ViewUI.LoadingBar.start();
  next();
});

router.afterEach(route => {
  ViewUI.LoadingBar.finish();
});


new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
