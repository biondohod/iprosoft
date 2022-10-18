document.addEventListener('DOMContentLoaded', () => {
    const openBtn = document.querySelector('.header__burger-btn');
    const closeBtn = document.querySelector('.burger-menu__close');
    const burgerMenu = document.querySelector('.burger-menu');

    const scrollWidth = calculateScrollWidth();

    const openBurgerMenu = () => {
        burgerMenu.classList.add('burger-menu--open');

        document.body.style.overflow = 'hidden';
        document.body.style.marginRight = `${scrollWidth}px`;

        closeBtn.addEventListener('click', closeBurgerMenu)
    }

    openBtn.addEventListener('click', openBurgerMenu);

    const closeBurgerMenu = () => {
        burgerMenu.classList.remove('burger-menu--open');

        document.body.style.overflow = '';
        document.body.style.marginRight = 0;

        closeBtn.removeEventListener('click', closeBurgerMenu);
    }

    function calculateScrollWidth() {
        const div = document.createElement('div');

        div.style.overflowY = 'scroll';
        div.style.width = '50px';
        div.style.height = '50px';
        div.style.visibility = 'hidden';

        document.body.append(div);

        const scrollWidth = div.offsetWidth - div.clientWidth;
        div.remove();

        return scrollWidth;
    }
});
