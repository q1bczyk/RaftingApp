import { EventEmitter, Injectable, signal } from '@angular/core';
import { environment } from '../../../../../../env/environment';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { SingleReservationDetailsType } from '../../../shared/types/api/reservation-types/reservation-details.type';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
    hubUrl : string = environment.hubsUrl + 'notification';
    private hubConnection? : HubConnection;
    newReservationReceived  = new EventEmitter<SingleReservationDetailsType>();

    createHubConnection()
    {
        this.hubConnection = new HubConnectionBuilder()
            .withUrl(this.hubUrl)
            .withAutomaticReconnect()
            .build();
        
        this.hubConnection.start()
            .then(() => {
                this.joinGroup();           
            })
            .catch(err => {
                console.log(err)
            })

        this.hubConnection.on("NewNotification", (newReservation: SingleReservationDetailsType) => {
            console.log(newReservation);
            this.newReservationReceived.emit(newReservation);
        });       
    }

    private joinGroup() 
    {
        if (this.hubConnection?.state === "Connected") 
            this.hubConnection.invoke("JoinGroup")
                .catch(error => {
                    console.error("Error while joining group:", error);
                });
        else 
            console.warn("Connection is not established. Cannot join group.");
          
    }
}
