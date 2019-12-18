document.addEventListener("DOMContentLoaded",function(){

$("#crss").on("click","div #myImg",function(){


var img2 = document.querySelectorAll("#img02");
for(var i = 0; i< img2.length; i++)
{
  img2[i].style.display = "none";
  if(img2[i].src == this.src)
  {
      img2[i].src = this.src
      img2[i].style.display = "block";
  }
}


var modal = document.getElementById("myModal");
modal.style.display = "block";
var span = document.getElementsByClassName("close")[0];
span.onclick = function() { 
modal.style.display = "none";
}

});


$('.owl-carousel').owlCarousel({
  items:1,
  loop:true,
  autoplay:true,
  margin:10,
  autoplayTimeout:4000,
  autoHeight:true,
});



});