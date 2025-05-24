import { Directive, Renderer2, ElementRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NgControl, FormControl } from "@angular/forms";
import { GlobalFunctionsService } from '../services/global/global-functions.service';

@Directive({
  selector: '[DateAutoComplete]'
})
export class DateAutoCompleteDirective {

  constructor(
    private renderer: Renderer2, private el: ElementRef,
    private globalFunctionsService: GlobalFunctionsService,
    public translate: TranslateService) {


  }

  // tslint:disable-next-line: use-lifecycle-interface
  ngAfterViewInit(): void {
    // this.el.nativeElement.changes.subscribe(controls => {
    //    this.applyDateAutoComplete(controls);
    // })
    if (this.el.nativeElement) {
      this.applyDateAutoComplete(this.el.nativeElement);
      this.validateCorrectDate(this.el.nativeElement);
    }
  }
  private applyDateAutoComplete(dateControl) {
    this.renderer.listen(dateControl, 'keypress', (e) => {
      //const regEx = new RegExp('^[0-9]*$');
      if (!(e.keyCode >= 48 && e.keyCode <= 57)) {
        event.preventDefault();
      }
      // directive logic here
      if (e.which !== 8) {
        let numChars = dateControl.value.length;
        if (numChars === 2 || numChars === 5) {
          let thisVal = dateControl.value;
          thisVal += '/';
          dateControl.value = thisVal;
        }
      }
    });
  }

  private validateCorrectDate(queryControls) {
    this.renderer.listen(queryControls, 'blur', () => {
      const date = queryControls.value;
      if (date !== '') {
        if (this.globalFunctionsService.checkValidDate(date) === false) {
          queryControls.value = '';
          //this.control.control.setValue(null);
          
        }
      }
    });
  }



}
