$(function () {
  $.getJSON('/dist/package.json', function (response) {
    $('.main-footer .hidden-xs').html('<b>Version</b> ' + response.version)
  })

  // Skin switcher
  var currentSkin = 'skin-black-light'

  $('#layout-skins-list [data-skin]').click(function (e) {
    e.preventDefault()
    var skinName = $(this).data('skin')
    $('body').removeClass(currentSkin)
    $('body').addClass(skinName)
    currentSkin = skinName
  })
})