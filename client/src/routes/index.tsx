import { createFileRoute } from "@tanstack/react-router";
import { useGetCatalog } from "./-api/use-get-catalog";
import { Search } from "./-components/search";

export const Route = createFileRoute("/")({
  component: RouteComponent,
});

function RouteComponent() {
  const { data: catalog } = useGetCatalog();

  return (
    <div className="container mx-auto">
      <Search catalog={catalog} />
    </div>
  );
}
