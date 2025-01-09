import { NavItemType } from "../../../types/ui/nav-item.type";

export const navItems : NavItemType[] = [
    {
        icon : 'bi bi-bar-chart-line',
        label : 'Dashboard',
        path : '/dashboard',
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
        icon : 'bi bi-people-fill',
        label : 'Konta',
        path : 'users',
    },
    {
        icon : 'bi bi-sliders2',
        label : 'Ustawienia',
        path : 'settings',
    },
]