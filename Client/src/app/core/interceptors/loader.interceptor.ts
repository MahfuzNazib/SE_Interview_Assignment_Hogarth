import { HttpInterceptorFn } from '@angular/common/http';
import { NgxSpinnerService } from 'ngx-spinner';
import { inject } from '@angular/core';
import { finalize } from 'rxjs/operators';

export const loaderInterceptor: HttpInterceptorFn = (req, next) => {
  const spinner = inject(NgxSpinnerService); // Inject the service in functional style
  spinner.show(); // Show the spinner

  return next(req).pipe(
    finalize(() => spinner.hide()) // Hide the spinner when the request completes
  );
};
