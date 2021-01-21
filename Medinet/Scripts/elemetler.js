
(function($){ 
    $.fn.extend({ 
	
		SYVTooltip: function() {
			
			
			var mainCont = this;
			
			
			this.hover(function() {
				
				mainCont.children('span').children('span').css({ display: 'block' });
				mainCont.children('span').stop().css({ display: 'block', opacity: 0, bottom: '30px' }).animate({ opacity: 1, bottom: '40px' }, 200);
				
			}, function() {
				
				mainCont.children('span').stop().animate({ opacity: 0, bottom: '50px' }, 200, function() {
					
					jQuery(this).hide();
					
				});
				
			});
			
		},
		
		kapaliBildirim: function() {
			
			
			var mainCont = this;
			
			
			this.children('span.kapali').click(function() {
				
				mainCont.slideUp(300);
				
			});
			
		},
		
		
		
		
		
		syvGecis: function() {
			
			
			var mainCont = this;
			
			if(mainCont.attr('class') == 'syvgecis-acik') { mainCont.children('.gecis-alan').css({ 'display': 'block' }) }
			
			
			mainCont.children('.gecis-isleyici').click(function() {
				
				if(mainCont.children('.gecis-alan').css('display') == 'none') {
					
					mainCont.removeClass('syvgecis-kapali').addClass('syvgecis-acik');
					mainCont.children('.gecis-alan').slideDown(200);
					
				} else {
					
					mainCont.children('.gecis-alan').slideUp(200);
					mainCont.removeClass('syvgecis-acik').addClass('syvgecis-kapali');
					
				}
				
			});
			
		},
		
		syvTabmenu: function() {
			
			//main container
			var mainCont = this;
			
			//categorizes our tabs
			var i = 1;
			mainCont.children('.tablar').children('li').each(function() {
				
				jQuery(this).addClass('tab_'+i);
				i++;
				
			});
			
			//categorizes our tabbed
			var i = 1;
			mainCont.children('.tabmenu').children('li').each(function() {
				
				jQuery(this).addClass('tabmenu_'+i);
				i++;
				
			});
			
			//set up the current tab and tabbed
			mainCont.children('.tablar').children('li:first').addClass('current');
			mainCont.children('.tabmenu').children('li:first').addClass('current');
			
			//when user clicks a tab
			mainCont.children('.tablar').children('li').click(function() {
				
				var tempClass = jQuery(this).attr('class').split(' ');
				
				if(tempClass[1] != 'current') {
					
					var myClass = tempClass[0].split('_');
					
					//removes the current
					mainCont.children('.tabmenu').children('li.current').removeClass('current');
					mainCont.children('.tablar').children('li.current').removeClass('current');
					
					//adds a new current
					mainCont.children('.tabmenu').children('li.tabmenu_'+myClass[1]).addClass('current');
					mainCont.children('.tablar').children('li.tab_'+myClass[1]).addClass('current');
					
				}
				
			});
			
		}
	
	});
	
})(jQuery);