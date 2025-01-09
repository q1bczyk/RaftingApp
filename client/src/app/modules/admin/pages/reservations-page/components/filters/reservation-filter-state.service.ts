import { Injectable, WritableSignal, signal } from "@angular/core";
import { ReservationFiltersType, filtersInit } from "./filters-init";
import { BehaviorSubject, Subject } from "rxjs";

@Injectable({
    providedIn: 'root',
})
export class ReservationFilterState {
    private activeFilters: ReservationFiltersType = filtersInit;
    private activeFiltersSubject: BehaviorSubject<ReservationFiltersType> = new BehaviorSubject(this.activeFilters);
    private filtersChanged: Subject<void> = new Subject<void>();

    getActiveFilters(): ReservationFiltersType {
        return this.activeFiltersSubject.value;
    }

    setStartDate(date: Date | null) {
        this.activeFilters = {
            ...this.activeFilters,
            startDate: date,
        };
        this.activeFiltersSubject.next(this.activeFilters);
    }

    setEndDate(date: Date | null): void {
        this.activeFilters = {
            ...this.activeFilters,
            endDate: date,
        };
        this.activeFiltersSubject.next(this.activeFilters);
    }

    setSpecificDate(date: Date | null): void {
        this.activeFilters = {
            ...this.activeFilters,
            specificDate: date,
        };
        this.activeFiltersSubject.next(this.activeFilters);
    }

    setId(id: string | null | undefined): void {
        this.activeFilters = {
            ...this.activeFilters,
            reservationId: !id || id?.length === 0 ? null : id,
            lastNamePartial: !id || id?.length === 0 ? this.activeFilters.lastNamePartial : null,
        };
        this.activeFiltersSubject.next(this.activeFilters);
    }

    setLastname(lastname: string | null | undefined): void {
        this.activeFilters = {
            ...this.activeFilters,
            lastNamePartial: !lastname || lastname?.length === 0 ? null : lastname,
            reservationId: !lastname || lastname?.length === 0 ? this.activeFilters.reservationId : null,
        };
        this.activeFiltersSubject.next(this.activeFilters);
    }

    onClear() {
        this.activeFilters = { ...filtersInit };
        this.activeFiltersSubject.next(this.activeFilters);
        this.filtersChanged.next();
    }

    notifyFiltersChanged(): void {
        this.filtersChanged.next();
    }

    getFiltersChangedObservable() {
        return this.filtersChanged.asObservable();
    }
}