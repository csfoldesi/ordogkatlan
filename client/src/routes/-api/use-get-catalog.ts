import { client, useAuthClient } from "@/client";
import { useQuery } from "@tanstack/react-query";
import type { AxiosError } from "axios";
import type { Catalog } from "../-models";

export const useGetCatalog = () => {
  const { setClientToken } = useAuthClient();

  return useQuery<Catalog, AxiosError>({
    queryKey: ["Catalog"],
    retry: false,
    staleTime: Infinity,
    queryFn: async () => {
      await setClientToken(client);
      return client
        .get(`/catalog`)
        .then((res) => {
          return res.data.data;
        })
        .catch((error: AxiosError) => {
          throw error;
        });
    },
  });
};
