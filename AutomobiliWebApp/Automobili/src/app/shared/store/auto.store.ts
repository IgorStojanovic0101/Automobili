import { computed, inject, WritableSignal } from '@angular/core';
import { signalStore, withState, withMethods, patchState, EmptyFeatureResult, SignalStoreFeature, withComputed, withHooks, signalStoreFeature } from '@ngrx/signals';

import { Observable, tap, catchError, of } from 'rxjs';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import {  withCrudOperations } from '../operations/crud.state';
import {  createDispatchMap, ActionMap } from '@ngxs/store';
import { AutoService } from '../services/post.service';
import { BaseState } from './states/base-state';
import { Auto } from '../viewModels/auto';

export interface PostState extends BaseState<Auto> {}

export const initialState: PostState = {
  items: [],
  loading: false,
};



export const AutoStore = signalStore(
  
  withState(initialState),
  withCrudOperations<Auto>(AutoService),
  withDevtools('Autos'),
  withMethods(() => ({
  })),
  withHooks({
    onInit({ getItems }) {
      console.log('on init');
      getItems()
    },
    onDestroy() {
      console.log('on destroy');
    },
    
  }
)
  
 
);

