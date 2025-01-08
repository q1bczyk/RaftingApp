import { Injectable, WritableSignal, signal } from "@angular/core";
import { ReservationFiltersType, filtersInit } from "./filters-init";
import { BehaviorSubject } from "rxjs";

@Injectable({
    providedIn: 'root',
})
export class ReservationFilterState {
    private activeFilters: ReservationFiltersType = filtersInit;
    private activeFiltersSubject: BehaviorSubject<ReservationFiltersType> = new BehaviorSubject(this.activeFilters);

    getActiveFilters() : ReservationFiltersType {
        return this.activeFiltersSubject.value;
    }

    setStartDate(date: Date | null) {
        this.activeFilters = {
            ...this.activeFilters,
            startDate: date,
        };
        this.activeFiltersSubject.next(this.activeFilters); 
    }
    
    setEndDate(date: Date | null) : void{
        this.activeFilters = {
            ...this.activeFilters,
            endDate: date,
        };
        this.activeFiltersSubject.next(this.activeFilters); 
    }

    setSpecificDate(date: Date | null) : void{
        this.activeFilters = {
            ...this.activeFilters,
            specificDate: date,
        };
        this.activeFiltersSubject.next(this.activeFilters); 
    }

    setId(id : string | null | undefined) : void{
        this.activeFilters = {
            ...this.activeFilters,
            reservationId: !id || id?.length === 0 ? null : id,
            lastNamePartial : !id || id?.length === 0 ? this.activeFilters.lastNamePartial : null,
        };
        this.activeFiltersSubject.next(this.activeFilters); 
    }

    setLastname(lastname : string | null | undefined) : void{
        this.activeFilters = {
            ...this.activeFilters,
            lastNamePartial: !lastname || lastname?.length === 0 ? null : lastname,
            reservationId: !lastname || lastname?.length === 0 ? this.activeFilters.reservationId : null,
        };
        this.activeFiltersSubject.next(this.activeFilters); 
    }

    getActiveFiltersObservable() {
        return this.activeFiltersSubject.asObservable();
    }
}