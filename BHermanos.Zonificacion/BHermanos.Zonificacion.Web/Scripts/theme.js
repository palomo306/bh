function InitializeScreen()
{    
	"use strict";

	var Core = {

		initialized: false,

		initialize: function() {

			if (this.initialized) return;
			this.initialized = true;

			this.build();

		},

		build: function() {

			// Adds window smooth scroll on chrome.
			if($("html").hasClass("chrome")) {
				$.smoothScroll();
			}

			// Toggle
			this.toggle();

		},

		events: function() {

			// Anchors Position
			$("a[data-hash]").on("click", function(e) {

				e.preventDefault();
				var header = $("#header"),
					headerHeight = header.height(),
					target = $(this).attr("href"),
					$this = $(this);

				if($(window).width() > 991) {
					$("html,body").animate({scrollTop: $(target).offset().top - (headerHeight + 50)}, 600, "easeOutQuad");
				} else {
					$("html,body").animate({scrollTop: $(target).offset().top - 30}, 600, "easeOutQuad");
				}

				return false;

			});

		},

		toggle: function() {

			var $this = this,
				previewParClosedHeight = 25;

			$("section.toggle > label").prepend($("<i />").addClass("icon icon-plus"));
			$("section.toggle > label").prepend($("<i />").addClass("icon icon-minus"));
			$("section.toggle.active > p").addClass("preview-active");
			$("section.toggle.active > div.toggle-content").slideDown(350, function() {});

			$("section.toggle > label").click(function(e) {

				var parentSection = $(this).parent(),
					parentWrapper = $(this).parents("div.toogle"),
					previewPar = false,
					isAccordion = parentWrapper.hasClass("toogle-accordion");

				if(isAccordion && typeof(e.originalEvent) != "undefined") {
					parentWrapper.find("section.toggle.active > label").trigger("click");
				}

				parentSection.toggleClass("active");

				// Preview Paragraph
				if(parentSection.find("> p").get(0)) {

					previewPar = parentSection.find("> p");
					var previewParCurrentHeight = previewPar.css("height");
					previewPar.css("height", "auto");
					var previewParAnimateHeight = previewPar.css("height");
					previewPar.css("height", previewParCurrentHeight);

				}

				// Content
				var toggleContent = parentSection.find("> div.toggle-content");

				if(parentSection.hasClass("active")) {

					$(previewPar).animate({
						height: previewParAnimateHeight
					}, 350, function() {
						$(this).addClass("preview-active");
					});

					toggleContent.slideDown(350, function() {});

				} else {

					$(previewPar).animate({
						height: previewParClosedHeight
					}, 350, function() {
						$(this).removeClass("preview-active");
					});

					toggleContent.slideUp(350, function() {});

				}

			});

		},

	};

	Core.initialize();

}