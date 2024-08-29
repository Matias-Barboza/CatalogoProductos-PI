let menuVisible = false;

function MostrarOcultar()
{
    if (menuVisible) {
        document.getElementById("ul-nav").classList = "";
        menuVisible = false;
    }
    else
    {
        let ulNavbar;
        let numChildren;

        ulNavbar = document.querySelector(".ul-navbar");
        numChildren = ulNavbar.children.length;

        document.getElementById("ul-nav").classList = "responsive";

        if (numChildren > 2)
        {
            document.getElementById("ul-nav").classList = "responsive-180";
        }

        menuVisible = true;
    }
}