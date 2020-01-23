// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



//-------------------------Add shadow effect on cards when hovered-------------

$(document).ready(function () {
  // executes when HTML-Document is loaded and DOM is ready

  $(".card").hover(
    function () {
      $(this).addClass('shadow-lg').css('cursor', 'pointer');
    }, function () {
      $(this).removeClass('shadow-lg');
    }
  );

  // document ready  
});
