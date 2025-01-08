export interface ReservationFiltersType{
    startDate? : Date,
    endDate? : Date,
    specificDate? : Date,
    lastNamePartial ? : string,
    reservationId? : string
  }
  
  export const filtersInit : ReservationFiltersType = {
    startDate : undefined,
    endDate : undefined,
    specificDate : new Date(),
    lastNamePartial : undefined,
    reservationId : undefined,
  }