export interface ReservationFiltersType{
    startDate? : Date | null,
    endDate? : Date | null,
    specificDate? : Date | null,
    lastNamePartial ? : string | null,
    reservationId? : string | null
  }
  
  export const filtersInit : ReservationFiltersType = {
    endDate : undefined,
    specificDate : null,
    lastNamePartial : undefined,
    reservationId : undefined,
  }