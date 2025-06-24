import { Stage } from './stage';

export type Village = {
  id: string;
  name: string;
  style?: string;
  stages: Stage[];
};
