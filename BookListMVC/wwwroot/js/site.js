// Add a custom class to navigation active page
$('.header-nav').find('[href="' + window.location.pathname + '"]').parent('.nav-item').addClass('active');
