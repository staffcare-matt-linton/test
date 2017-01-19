jQuery(function () {
        $("#search").focus();
        $("form").submit();

        $("#search").keyup(function () {
            $("form").submit();
        });
    }
)

/*
_Layout.cshtml
    @RenderSection("scripts", required: false)
	</body>
	</html>


Ajax.cshtml
	@section scripts {
		<script src="~/Scripts/jquery.unobtrusive-ajax.js" type="text/javascript"></script>
		<script src="~/Scripts/Search.js" type="text/javascript"></script>
	}
*/