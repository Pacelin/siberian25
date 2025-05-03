using System.Collections.Generic;
using System.Linq;
using Siberian25.Game.Characters;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Siberian25.Game.Bullets
{
    public class DeflectedBulletEngine : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _ps;
        [SerializeField] private ParticleSystem _deflectedBullets;

        private readonly List<ParticleCollisionEvent> _collisionEvents = new();

        private void OnParticleCollision(GameObject other)
        {
            // Gather collisions and particles
            int collisionEventsSize = _ps.GetSafeCollisionEventSize();
            for (int i = 0; i < collisionEventsSize; i++)
                _collisionEvents.Add(default);
            _ps.GetCollisionEvents(other, _collisionEvents);

            NativeList<ParticleSystem.Particle> particleList = GetParticleList(_ps);
            NativeList<ParticleSystem.Particle> deflectedParticleList = GetParticleList(_deflectedBullets);

            // Try get player
            PlayerSliceTrigger player = other.GetComponentInChildren<PlayerSliceTrigger>();

            HashSet<int> removedParticles = new();

            // Deflect or destroy
            foreach (ParticleCollisionEvent evt in _collisionEvents)
            {
                // Find who we collided with
                int closestParticleIndex = GetClosestParticle(particleList, evt);

                // Deflect: player is hit while dash. player is ignored after that. Remove from enemy, add to deflected
                if (player != null && player.EnableSlice)
                {
                    // Deflect particle velocity
                    Vector3 velocity = player.SliceDirection * _ps.main.startSpeed.Evaluate(Random.value);
                    ParticleSystem.Particle deflectedParticle = GetDeflectedParticle(particleList, closestParticleIndex, velocity);
                    deflectedParticle.startColor = _deflectedBullets.main.startColor.Evaluate(Random.value);
                    // It will be removed from enemy ps. Add to deflected ps
                    AddParticle(deflectedParticleList, deflectedParticle);
                }
                // Destroy: this is not a player or player is not dashing. Cause damage to whatever this is
                else
                {
                    // Deal damage
                    if (other.TryGetComponent(out HealthComponent health))
                        health.TakeDamage(1);
                }

                removedParticles.Add(closestParticleIndex);
            }

            IOrderedEnumerable<int> removedParticlesSorted = removedParticles.OrderByDescending(a => a);
            foreach (int index in removedParticlesSorted)
                RemoveParticle(particleList, index);

            _ps.SetParticles(particleList.AsArray(), particleList.Length);
            _deflectedBullets.SetParticles(deflectedParticleList.AsArray(), deflectedParticleList.Length);

            _collisionEvents.Clear();
        }

        private static NativeList<ParticleSystem.Particle> GetParticleList(ParticleSystem ps)
        {
            int particlesLength = ps.particleCount;
            NativeArray<ParticleSystem.Particle> particles = new(particlesLength, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
            ps.GetParticles(particles, particlesLength);

            NativeList<ParticleSystem.Particle> particleList = new(particlesLength, Allocator.Temp);
            particleList.AddRange(particles);

            return particleList;
        }

        private static int GetClosestParticle(in NativeList<ParticleSystem.Particle> particles, ParticleCollisionEvent evt)
        {
            int index = 0;
            float minSqrDistance = (particles[0].position - evt.intersection).sqrMagnitude;

            for (int i = 1; i < particles.Length; i++)
            {
                ParticleSystem.Particle particle = particles[i];
                float sqrDistance = (particle.position - evt.intersection).sqrMagnitude;

                if (sqrDistance < minSqrDistance)
                {
                    index = i;
                    minSqrDistance = sqrDistance;
                }
            }

            return index;
        }

        private static void RemoveParticle(NativeList<ParticleSystem.Particle> particles, int index)
        {
            particles.RemoveAt(index);
        }

        private static void AddParticle(NativeList<ParticleSystem.Particle> particles, ParticleSystem.Particle particle)
        {
            particles.Add(particle);
        }

        private static ParticleSystem.Particle GetDeflectedParticle(NativeList<ParticleSystem.Particle> particles, int index, Vector3 velocity)
        {
            ParticleSystem.Particle particle = particles[index];
            particle.velocity = velocity;
            particle.remainingLifetime = particle.startLifetime;
            return particle;
        }
    }
}
