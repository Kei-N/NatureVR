using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

namespace NatureVR.Utility
{
    public class InitialSettings
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void RegisterResolvers()
        {
            // NOTE: Currently, CompositeResolver doesn't work on Unity IL2CPP build. Use StaticCompositeResolver instead of it.
            StaticCompositeResolver.Instance.Register(
                //MessagePack.Resolvers.GeneratedResolver.Instance, // MessagePackをGenerateする必要がある
                BuiltinResolver.Instance,
                PrimitiveObjectResolver.Instance,
                MessagePack.Unity.UnityResolver.Instance
            );

            MessagePackSerializer.DefaultOptions = MessagePackSerializer.DefaultOptions
                .WithResolver(StaticCompositeResolver.Instance);
        }
    }
}