import { NavItemType } from "../../../types/ui/nav-item.type";

export const navItems : NavItemType[] = [
    {
        icon : 'bi bi-columns-gap',
        label : 'Dashboard',
        path : '/admin',
    },
    {
        icon : 'bi bi-calendar-check',
        label : 'Rezerwacje',
        path : 'bookings',
    },
    {
        icon : 'bi bi-tsunami',
        label : 'Sprzęt',
        path : 'equipment',
    },
    {
        icon : 'bi bi-gift',
        label : 'Promocje',
        path : 'discounts',
    },
    {
        icon : 'bi bi-cash-coin',
        label : 'Płatności',
        path : 'payments',
    },
    {
        icon : 'bi bi-people-fill',
        label : 'Konta',
        path : 'users',
    },
    {
        icon : 'bi bi-gear',
        label : 'Ustawienia',
        path : 'settings',
    },
]