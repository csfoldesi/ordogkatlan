import type { Genre } from "./genre";

export type Performance = {
  id: string;
  title: string;
  description?: string;
  thumbnail?: string;
  productionId: string;
  date: string;
  startTime?: string;
  endTime?: string;
  duration: number;
  villageId: string;
  villageName: string;
  stageId: string;
  stageName: string;
  isSelected: boolean;
  isTicketed: boolean;
  genres: Genre[];
};
