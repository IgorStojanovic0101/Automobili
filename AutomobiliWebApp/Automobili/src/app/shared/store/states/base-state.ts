import { Entity } from "@angular-architects/ngrx-toolkit";


export type BaseState<Entity> = {
  items: Entity[];
  loading: boolean;
};

export type ItemBaseState<Entity> = {
    item: Entity;
    loading: boolean;
  };


export interface UserState<Entity> {
    item: Entity | null;
    loading: boolean;
}


