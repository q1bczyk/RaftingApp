import { NavItemType } from "../../../types/ui/nav-item.type";

export const navItems : NavItemType[] = [
    {
        icon : 'bi bi-columns-gap',
        label : 'Dashboard',
        path : '/admin',
    },
    {
        icon : 'bi bi-credit-card-2-back',
        label : 'Płatności',
        path : 'payments',
    }
]