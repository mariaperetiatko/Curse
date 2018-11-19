// import 'rxjs/Rx'; // adds ALL RxJS statics & operators to Observable

// See node_module/rxjs/Rxjs.js
// Import just the rxjs statics and operators we need for THIS app.

// Statics
import 'rxjs/add/observable/throw';

// Operators
import 'rxjs/add/operators/catch';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operators/map';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/toPromise';
