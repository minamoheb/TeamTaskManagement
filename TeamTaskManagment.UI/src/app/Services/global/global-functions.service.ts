import { Injectable, Inject } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { Observable, Subscription } from 'rxjs';
import { ConfirmationDialogComponent } from 'src/app/components/widgets/confirmation-dialog/confirmation-dialog.component';


import { environment } from '../../../environments/environment';
import { ConfirmDialogType } from 'src/app/models/global';
import { DOCUMENT } from '@angular/common';

@Injectable({
    providedIn: 'root',
})
export class GlobalFunctionsService {
    //#region variables
    myComponent: any;
    pageMode: string;
    confirmationDialogRef: MatDialogRef<ConfirmationDialogComponent>;

    subscriptions: Subscription[] = [];

    constructor(
        private translate: TranslateService,
        private dialog: MatDialog,
        @Inject(DOCUMENT) private document: Document) {

    }
    //#endregion
    // *********************** Show Message Dialog **************************
    //#region Show Message Dialog
    openShowMessageDialog(message: string, actiontype: any): void {
        this.confirmationDialogRef = this.dialog.open(ConfirmationDialogComponent, {
            data: { type: ConfirmDialogType.Info, msg: message },
        });
        this.subscriptions.push(
            this.confirmationDialogRef.afterClosed().subscribe((result) => {
                if (result) {
                    this.myComponent.showMessageOkClicked(actiontype);
                } else {
                    this.myComponent.showMessageCancelClicked(actiontype);
                }
            }));
    }


    //#endregion
    //#region  Functions
    // ******************************set Global variables  ************************

    disposed(): void {
        this.subscriptions.forEach((subscription) => subscription.unsubscribe);
    }

    checkValidDate(date): boolean {
        let dateRegEx: any;
        dateRegEx = null;
        // tslint:disable-next-line: max-line-length

        dateRegEx = new RegExp(/^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/g);
        if (dateRegEx.test(date)) {
        } else {
            //const ErrorsList: Array<any> = [];
            //ErrorsList.push(this.translate.instant('MSG010'));
            //this.openValidationDialog(ErrorsList);
            return false;
        }
        return true;
    }

    ConvertToDate(dateStr) {
        if (dateStr) {
            const parts = dateStr.split('/');
            return parts[1] + '/' + parts[0] + '/' + parts[2];
        } else {
            return dateStr;
        }
    }


}
function moment(datestr: string, arg1: string) {
    throw new Error('Function not implemented.');
}

