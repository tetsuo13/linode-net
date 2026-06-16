using Linode.Models;
using Linode.Models.Volumes;

namespace Linode.Tests.TestHelpers.Models;

public static class VolumeModelHelper
{
    public const string DefaultVolumeJsonResponse = """
                                                     {
                                                       "created": "2025-01-01T00:01:01",
                                                       "encryption": "enabled",
                                                       "filesystem_path": "/dev/disk/by-id/scsi-0Linode_Volume_my-volume",
                                                       "hardware_type": "nvme",
                                                       "id": 12345,
                                                       "io_ready": true,
                                                       "label": "Video-file-storage",
                                                       "linode_id": 12346,
                                                       "linode_label": "linode123",
                                                       "locks": [
                                                         "cannot_delete"
                                                       ],
                                                       "region": "us-iad",
                                                       "size": 30,
                                                       "status": "active",
                                                       "tags": [
                                                         "blk-stg-volume-1",
                                                         "videos-storage"
                                                       ],
                                                       "updated": "2025-01-01T00:01:01"
                                                     }
                                                     """;

    public static readonly Volume DefaultVolume = new()
    {
        Created = new DateTime(2025, 1, 1, 0, 1, 1),
        Toggle = ToggleType.Enabled,
        FileSystemPath = "/dev/disk/by-id/scsi-0Linode_Volume_my-volume",
        HardwareType = HardwareType.Nvme,
        Id = 12345,
        IoReady = true,
        Label = "Video-file-storage",
        LinodeId = 12346,
        LinodeLabel = "linode123",
        Locks = ["cannot_delete"],
        Region = "us-iad",
        Size = 30,
        Status = VolumeStatus.Active,
        Tags =
        [
            "blk-stg-volume-1",
            "videos-storage"
        ],
        Updated = new DateTime(2025, 1, 1, 0, 1, 1)
    };
}
