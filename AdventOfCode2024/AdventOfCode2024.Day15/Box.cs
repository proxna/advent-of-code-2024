namespace AdventOfCode2024.Day15
{
    internal class Box
    {
        public Box(int x, int y, bool part2)
        {
            Location = (x, y);
            LocationEnd = part2 ? (x, y + 1) : (x, y);
        }

        public (int x, int y) Location { get; set; }

        public (int x, int y) LocationEnd { get; set; }

        public int Distance { get => Location.x * 100 + Location.y; }

        public bool TryGetMove((int x, int y) direction, List<Box> boxes, HashSet<(int x, int y)> walls)
        {
            var newLocationCandidate = (Location.x + direction.x, Location.y + direction.y);
            var neighborBox = boxes.FirstOrDefault(bx => bx.Location == newLocationCandidate);

            if (neighborBox != null)
            {
                if (neighborBox.TryGetMove(direction, boxes, walls))
                {
                    Location = newLocationCandidate;
                    return true;
                }

                return false;
            }

            if (walls.Contains(newLocationCandidate))
                return false;

            Location = newLocationCandidate;
            return true;
        }

        public bool TryGetMovePart2((int x, int y) direction, List<Box> boxes, HashSet<(int x, int y)> walls, List<Box> movingBoxes)
        {
            ((int x, int y) start, (int x, int y) end) newLocationCandidates = ((Location.x + direction.x, Location.y + direction.y), (LocationEnd.x + direction.x, LocationEnd.y + direction.y));
            var neighborBox = boxes.Where(bx => (bx.Location == newLocationCandidates.start
                || bx.LocationEnd == newLocationCandidates.start
                || bx.Location == newLocationCandidates.end
                || bx.LocationEnd == newLocationCandidates.end)
                && bx != this && !movingBoxes.Contains(bx)).ToList();

            if (neighborBox.Any())
            {
                if (neighborBox.All(bx => bx.TryGetMovePart2(direction, boxes, walls, neighborBox)))
                {
                    Location = newLocationCandidates.start;
                    LocationEnd = newLocationCandidates.end;
                    return true;
                }

                return false;
            }

            if (walls.Where(wal => wal == newLocationCandidates.start || wal == newLocationCandidates.end
                || (wal.x, wal.y + 1) == newLocationCandidates.start || (wal.x, wal.y + 1) == newLocationCandidates.end).Any())
                return false;

            Location = newLocationCandidates.start;
            LocationEnd = newLocationCandidates.end;
            return true;
        }
    }
}
